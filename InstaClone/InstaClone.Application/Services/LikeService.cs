using AutoMapper;
using InstaClone.Application.DTOs.LikeDTOs;
using InstaClone.Application.Exceptions;
using InstaClone.Application.Interfaces;
using InstaClone.Domain.Entities;
using InstaClone.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.Services
{
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LikeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<LikeResponseDto>> GetAllAsync()
        {
            return _mapper.Map<IReadOnlyList<LikeResponseDto>>(await _unitOfWork.Likes.GetAllAsync());
        }

        public async Task<LikeResponseDto?> GetOneAsync(Guid id)
        {
            var like = await _unitOfWork.Likes.GetOneAsync(id);
            if (like == null)
            {
                throw new NotFoundException(id.ToString());
            }
            return _mapper.Map<LikeResponseDto>(like);
        }

        public async Task CreateAsync(LikeResponseDto likeDto)
        {
            var like = _mapper.Map<Like>(likeDto);
            await _unitOfWork.Likes.AddAsync(like);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(LikeResponseDto likeDto)
        {
            var like = await _unitOfWork.Likes.GetOneAsync(likeDto.Id);
            if (like == null)
            {
                throw new NotFoundException(likeDto.Id.ToString());
            }
            _mapper.Map(likeDto, like);
            _unitOfWork.Likes.Update(like);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var like = await _unitOfWork.Likes.GetOneAsync(id);
            if (like == null)
            {
                throw new NotFoundException(id.ToString());
            }
            _unitOfWork.Likes.Delete(like);
            await _unitOfWork.CompleteAsync();
        }

    }
}
