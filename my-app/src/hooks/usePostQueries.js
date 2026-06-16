import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { postService } from '../services/post.service';

export function useGetUserFeed() {
  return useQuery({
    queryKey: ['postsFeed'],
    queryFn: async() => {
      return await postService.getUserFeed();
    },
  })
};

export function useGetPostsByUser() {
  return useQuery({
    queryKey: ['postsUser'],
    queryFn: async() => {
      return await postService.getPostsByUser();
    },
  });
};

export function useGetPostById(id) {
  return useQuery({
    queryKey: ['post', id],
    queryFn: async() => {
      return await postService.getOne(id);
    },
  })
};
export function useCreatePost() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (data) => {
      return await postService.create(data)
    },
 })
}

export function useDeletePost() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (id) => {
      return await postService.delete(id)
    },
  })
}

//
export function useLikePost() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (postId) => {
      return await postService.likePost(postId)
    },
  })
}

export function useUnlikePost() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (postId) => {
      return await postService.unlikePost(postId)
    },
  })
}

export function useCreateComment() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async ({postId, data}) => {
      return await postService.createComment(postId, data)
    },
    onSuccess: (data) => {
      return data;
    },
    
  })
}

export function useCreateReplyComment() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async ({postId, commentId, data}) => {
      return await postService.createReplyComment(postId, commentId, data)
    },
  })
}