using AutoMapper;
using InstaClone.Application.DTOs.CommentDTOs;
using InstaClone.Application.DTOs.LikeDTOs;
using InstaClone.Application.DTOs.PostDTOs;
using InstaClone.Application.Exceptions;
using InstaClone.Application.Interfaces;
using InstaClone.Domain.Entities;
using InstaClone.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ILocalImageStorageService _localImageStorageService;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, ILocalImageStorageService localImageStorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _localImageStorageService = localImageStorageService;
        }

        public async Task<IReadOnlyList<PostSummaryResponseDto>> GetUserFeedAsync(ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            if (user == null)
                throw new UnauthorizedAccessException();

            var feedRaw = await _unitOfWork.Posts.GetUserFeedAsync(user.Id);

            var likedOnlyIds = await _unitOfWork.Posts.FilterLikedByUser(user.Id, feedRaw);

            var mappedFeed = _mapper.Map<IReadOnlyList<PostSummaryResponseDto>>(feedRaw);

            foreach (var item in mappedFeed)
            {
                if (likedOnlyIds.Contains(item.Id))
                {
                    item.IsLiked = true;
                }
            }

            return mappedFeed;
        }

        public async Task<IReadOnlyList<PostSummaryResponseDto>> GetAllAsync()
        {
            return _mapper.Map<IReadOnlyList<PostSummaryResponseDto>>(await _unitOfWork.Posts.GetAllAsync());
        }

        public async Task<IReadOnlyList<PostSummaryResponseDto>> GetByUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            if (user == null)
                throw new UnauthorizedAccessException();

            var postsRaw = await _unitOfWork.Posts.GetPostsByUserAsync(user.Id);

            var likedOnlyIds = await _unitOfWork.Posts.FilterLikedByUser(user.Id, postsRaw);

            var mappedPosts = _mapper.Map<IReadOnlyList<PostSummaryResponseDto>>(postsRaw);

            foreach (var item in mappedPosts)
            {
                if (likedOnlyIds.Contains(item.Id))
                {
                    item.IsLiked = true;
                }
            }

            return mappedPosts;
        }

        public async Task<PostDetailResponseDto?> GetOneAsync(Guid id)
        {
            var post = await _unitOfWork.Posts.GetOneAsync(id);
            if (post == null)
            {
                throw new NotFoundException(id.ToString());
            }
            return _mapper.Map<PostDetailResponseDto>(post);
        }

        public async Task CreateAsync(PostCreateRequestDto postDto, IFormFile? file, ClaimsPrincipal claimsPrincipal)
        {
            
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            if (user == null)
                throw new UnauthorizedAccessException();
            
            var post = _mapper.Map<Post>(postDto);
            post.AuthorId = user.Id;
            await _unitOfWork.Posts.AddAsync(post);
            await _unitOfWork.CompleteAsync();


            if (file == null)
                throw new BadRequestException("File is empty!");

            await AddImageAsync(post, file);
        }

        public async Task UpdateAsync(PostUpdateRequestDto postDto, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            if (user == null)
                throw new UnauthorizedAccessException();

            var post = await _unitOfWork.Posts.GetOneAsync(postDto.Id);
            if (post == null)
            {
                throw new NotFoundException(postDto.Id.ToString());
            }

            if (user.Id != post.Id)
                throw new UnauthorizedAccessException();

            _mapper.Map(postDto, post);
            _unitOfWork.Posts.Update(post);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(Guid id, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            if (user == null)
                throw new UnauthorizedAccessException();

            var post = await _unitOfWork.Posts.GetOneAsync(id);
            if (post == null)
            {
                throw new NotFoundException(id.ToString());
            }

            if (user.Id != post.Id)
                throw new UnauthorizedAccessException();

            _unitOfWork.Posts.Delete(post);
            await _unitOfWork.CompleteAsync();
        }

        public async Task LikePostAsync(Guid postId, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            if (user == null)
                throw new UnauthorizedAccessException();

            var post = await _unitOfWork.Posts.GetOneAsync(postId);
            if (post == null)
                throw new NotFoundException(postId.ToString());

            if (user.Id != post.AuthorId)
                throw new UnauthorizedAccessException();


            Like like = new Like
            {
                PostId = postId,
                LikerId = user.Id
            };

            await _unitOfWork.Likes.AddAsync(like);
            await _unitOfWork.CompleteAsync();
        }
        public async Task UnlikePostAsync(Guid postId, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            if (user == null)
                throw new UnauthorizedAccessException();

            var post = await _unitOfWork.Posts.GetOneAsync(postId);
            if (post == null)
                throw new NotFoundException("Post with id:" + postId.ToString());

            if (user.Id != post.AuthorId)
                throw new UnauthorizedAccessException();

            var like = await _unitOfWork.Likes.GetOneByUserAndPostAsync(postId, user.Id);
            if (like == null)
                throw new NotFoundException("Like with id:" + postId.ToString());

            post.Likes.Remove(like);
            _unitOfWork.Likes.Delete(like);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<CommentResponseDto> CommentOnPostAsync(Guid postId, CommentCreateRequestDto comment, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            if (user == null)
                throw new UnauthorizedAccessException();

            var post = await _unitOfWork.Posts.GetOneAsync(postId);
            if (post == null)
                throw new NotFoundException("Post with id:" + postId.ToString());

            if (user.Id != post.AuthorId)
                throw new UnauthorizedAccessException();

            var newComment = _mapper.Map<Comment>(comment);
            newComment.PostId = postId;
            newComment.AuthorId = user.Id;

            await _unitOfWork.Comments.AddAsync(newComment);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<CommentResponseDto>(newComment);
        }
        public async Task<ReplyResponseDto> ReplyOnCommentOnThisPostAsync(Guid postId, Guid commentId, ReplyCreateRequestDto reply, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            if (user == null)
                throw new UnauthorizedAccessException();

            var post = await _unitOfWork.Posts.GetOneAsync(postId);
            if (post == null)
                throw new NotFoundException("Post with id:" + postId.ToString());

            if (user.Id != post.AuthorId)
                throw new UnauthorizedAccessException();

            var comment = await _unitOfWork.Comments.GetOneAsync(commentId);
            if (comment == null)
                throw new NotFoundException("Comment with id:" + commentId.ToString());

            var newReply = _mapper.Map<ReplyComment>(reply);
            newReply.RepliedCommentId = commentId;
            newReply.AuthorId = user.Id;

            await _unitOfWork.ReplyComments.AddAsync(newReply);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ReplyResponseDto>(newReply);
        }

        private async Task AddImageAsync(Post post, IFormFile file)
        {
            var imageUrl = await _localImageStorageService.SavePostImageAsync(file, post.Id);
            post.Photo = imageUrl;
            await _unitOfWork.CompleteAsync();
        }
    }
}
