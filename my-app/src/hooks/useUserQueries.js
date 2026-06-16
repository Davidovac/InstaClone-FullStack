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
