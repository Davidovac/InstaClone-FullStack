import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { authService } from '../services/auth.service';
import { useAuthStore } from '../store/useAuthStore';

export function useLogin() {
  const queryClient = useQueryClient();
  const setAuth = useAuthStore((state) => state.setAuth);
  
  return useMutation({
    mutationFn: async (data) => {
      try {
        return await authService.login(data);
      } catch (error) {
        throw new Error(errorHandler(error));
      }
    },
    onSuccess: (response) => {
      if (response.data.token) {
        setAuth(response.data.token, response.data.user);
      }
    }
  })
}

export function useRegister() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (data) => {
      try {
        return await authService.register(data);
      } catch (error) {
        throw new Error(errorHandler(error));
      }
    },
    onSuccess: (response) => {
      alert("Uspesno ste se registrovali! Proverite email za aktivaciju naloga.");
    },
  })
};

export function useActivateAccount() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (data) => {
      try {
        return await authService.activateAccount(data);
      } catch (error) {
        throw new Error(errorHandler(error));
      }
    },
    onSuccess: (response) => {
      alert("Nalog je aktiviran! Sada se možete prijaviti.");
    },
  })
};

export function useForgotPassword() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (data) => {
      try {
        return await authService.forgotPassword(data);
      } catch (error) {
        throw new Error(errorHandler(error));
      }
    },
    onSuccess: () => {
      alert("Proverite email za link za resetovanje lozinke.");
    },
  })
};

export function useResetPassword() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async (data) => {
      try {
        return await authService.resetPassword(data);
      } catch (error) {
        throw new Error(errorHandler(error));
      }
    },
    onSuccess: () => {
      alert("Lozinka je uspešno resetovana.");
    },
  })
};