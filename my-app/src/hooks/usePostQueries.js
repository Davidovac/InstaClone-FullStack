import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { postService } from '../services/post.service';
import { errorHandler } from './handlers/errorHandler';

export function useGetUserFeed() {
  return useQuery({
    queryKey: ['postsFeed'],
    queryFn: async() => {
      try {
        return await postService.getUserFeed();
      }
      catch (error) {
        throw new Error(errorHandler(error));
      }
    },
  });
}

export function useGetPostsByUser() {
  return useQuery({
    queryKey: ['postsUser'],
    queryFn: async() => {
      try {
        return await postService.getPostsByUser();
      }
      catch (error) {
        throw new Error(errorHandler(error));
      }
    },
  });
}

export function useGetPostById(id) {
  return useQuery({
    queryKey: ['post', id],
    queryFn: async() => {
      try {
        return await postService.getOne(id);
      }
      catch (error) {
        throw new Error(errorHandler(error));
      }
    },
  });
}

export function useCreatePost() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (data) => {
      try {
        return await postService.create(data)
      } catch (error) {
        throw new Error(errorHandler(error));
      }
    },
    onError: (error) => {
      const backendMessage = error.response?.data?.message || "Something went wrong";
      console.error(backendMessage);
    }
  });
}

export function useDeletePost() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (id) => {
      try {
        return await postService.delete(id)
      } catch (error) {
        throw new Error(errorHandler(error));
      }
    },
    onError: (error) => {
      const backendMessage = error.response?.data?.message || "Something went wrong";
      console.error(backendMessage);
    }
  });
}

//
export function useLikePost() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (postId) => {
      try {
        return await postService.likePost(postId)
      } catch (error) {
        throw new Error(errorHandler(error));
      }
    },
    onError: (error) => {
      const backendMessage = error.response?.data?.message || "Something went wrong";
      console.error(backendMessage);
    }
  });
}

export function useUnlikePost() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (postId) => {
      try {
        return await postService.unlikePost(postId)
      } catch (error) {
        throw new Error(errorHandler(error));
      }
    },
    onError: (error) => {
      const backendMessage = error.response?.data?.message || "Something went wrong";
      console.error(backendMessage);
    }
  });
}

export function useCreateComment() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async ({postId, data}) => {
      try {
        return await postService.createComment(postId, data)
      } catch (error) {
        throw new Error(errorHandler(error));
      }
    },
    onSuccess: (data) => {
      return data;
    },
    
    onError: (error) => {
      const backendMessage = error.response?.data?.message || "Something went wrong";
      console.error(backendMessage);
    }
  });
}

export function useCreateReplyComment() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async ({postId, commentId, data}) => {
      try {
        return await postService.createReplyComment(postId, commentId, data)
      } catch (error) {
        throw new Error(errorHandler(error));
      }
    },
    onError: (error) => {
      const backendMessage = error.response?.data?.message || "Something went wrong";
      console.error(backendMessage);
    }
  });
}