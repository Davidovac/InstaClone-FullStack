import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { authService } from '../services/auth.service';
import { useAuthStore } from '../store/useAuthStore';

export function useLogin() {
  const queryClient = useQueryClient();
  const setAuth = useAuthStore((state) => state.setAuth);
  return useMutation({
    mutationFn: ({ userName, password }) => authService.login(userName, password),
    onSuccess: (response) => {
      if (response.data.token) {
        setAuth(response.data.token, response.data.user);
      }
      queryClient.invalidateQueries({ queryKey: ['login'] });
    },
    onError: (error) => {
      const backendMessage = error.response?.data?.message || "Something went wrong";
      console.error(backendMessage);
      return Promise.reject(new Error(backendMessage));
    }
  });
}

export function useRegister() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ userName, password, email, firstName, lastName }) => 
      authService.register(userName, password, email, firstName, lastName),
    onSuccess: (response) => {
      alert("Uspesno ste se registrovali! Proverite email za aktivaciju naloga.");
      queryClient.invalidateQueries({ queryKey: ['register'] });
    },
    onError: (error) => {
      const backendMessage = error.response?.data?.message || "Something went wrong";
      console.error(backendMessage);
      return Promise.reject(new Error(backendMessage));
    }
  });
}

export function useActivateAccount() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ email, token }) => authService.activateAccount(email, token),
    onSuccess: (response) => {
      alert("Nalog je aktiviran! Sada se možete prijaviti.");
      queryClient.invalidateQueries({ queryKey: ['activateAccount'] });
    },
    onError: (error) => {
      const backendMessage = error.response?.data?.message || "Something went wrong";
      console.error(backendMessage);
      return Promise.reject(new Error(backendMessage));
    }
  });
}