import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { userService } from '../services/user.service';

export function useGetUsers() {
  return useQuery({
    queryKey: ['users'],
    queryFn: ({ signal }) => userService.getAll(signal),
    onError: (error) => {
      const backendMessage = error.response?.data?.message || "Something went wrong";
      console.error(backendMessage);
    },
    staleTime: 5 * 60 * 1000, // Optional: Keep data fresh for 5 minutes
  });
}

export function useGetUser(id) {
  return useQuery({
    queryKey: ['user', id],
    queryFn: ({ signal }) => userService.getById(id, signal),
    onError: (error) => {
      const backendMessage = error.response?.data?.message || "Something went wrong";
      console.error(backendMessage);
    },
    staleTime: 5 * 60 * 1000, // Optional: Keep data fresh for 5 minutes
  });
}

export function useCreateUser() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (name) => userService.create(name),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['users'] });
    },
    onError: (error) => {
      const backendMessage = error.response?.data?.message || "Something went wrong";
      console.error(backendMessage);
    }
  });
}