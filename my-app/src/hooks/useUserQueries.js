import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { userService } from '../services/user.service';

export function useGetUsers(queryUsername = "") {
  return useQuery({
    queryKey: ['users', queryUsername],
    queryFn: ({ signal }) => userService.getAll(signal, queryUsername),
  });
}

export function useGetUser(id) {
  return useQuery({
    queryKey: ['user', id],
    queryFn: async () => {
      return await userService.getById(id);
    },
  });
}


export function useCreateUser() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (name) => {
      try {
        return await userService.create(name)
      } catch (error) {
        throw new Error(errorHandler(error));
      }
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['users'] });
    },
 })
}

export function useDeleteUser() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (id) => {
      try {
        return await userService.delete(id);
      } catch (error) {
        throw new Error(errorHandler(error));
      }
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['users'] });
      
    },
  });
}

export function useUpdateUser() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (userData) => {
      try {
        return await userService.update(userData);
      } catch (error) {
        throw new Error(errorHandler(error));
      }
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['users'] });
    },
  });
}

export function useFollowProfile() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (userName) => {
      try {
        return await userService.followProfile(userName)
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

export function useUnfollowProfile() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (userName) => {
      try {
        return await userService.unfollowProfile(userName)
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

export function useGetFollowersByUser(id) {
  return useQuery({
    queryKey: ['followers', id],
    queryFn: async () => {
      return await userService.getFollowersByUser(id);
    },
  });
}

export function useGetFollowingByUser(id) {
  return useQuery({
    queryKey: ['following', id],
    queryFn: async () => {
      return await userService.getFollowingByUser(id);
    },
  });
}


export function useFollowsProfile(userName) {
  return useQuery({
    queryKey: ['follows', userName],
    queryFn: async () => {
      return await userService.followsProfile(userName);
    },
  });
}

export function useGetProfile(userName) {
  return useQuery({
    queryKey: ['profile', userName],
    queryFn: async () => {
      return await userService.getProfile(userName);
    },
  });
}