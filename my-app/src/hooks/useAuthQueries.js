import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { authService } from '../services/auth.service';
import { useAuthStore } from '../store/useAuthStore';

const errorHandler = (error) => {
  const backendMessage = error.response?.status && error.response?.status != 500 ? error.response?.data?.detail : "Server error";
  console.error(backendMessage);
  return backendMessage;
}

export function useLogin() {
  const queryClient = useQueryClient();
  const setAuth = useAuthStore((state) => state.setAuth);
  
  return useMutation({
    mutationFn: async ({ userName, password }) => {
      try {
        return await authService.login(userName, password);
      } catch (error) {
        throw new Error(errorHandler(error));
      }
    },
    onSuccess: (response) => {
      if (response.data.token) {
        setAuth(response.data.token, response.data.user);
      }
      queryClient.invalidateQueries({ queryKey: ['login'] });
    },
  });
}

export function useRegister() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async ({ userName, password, email, firstName, lastName }) => {
      try {
        return await authService.register(userName, password, email, firstName, lastName);
      } catch (error) {
        throw new Error(errorHandler(error));
      }
    },
    onSuccess: (response) => {
      alert("Uspesno ste se registrovali! Proverite email za aktivaciju naloga.");
      queryClient.invalidateQueries({ queryKey: ['register'] });
    }});
    /*onError: (error) => {
      return Promise.reject(errorHandler(error));
    }
  });*/
}

export function useActivateAccount() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: async ({ email, token }) => {
      try {
        return await authService.activateAccount(email, token);
      } catch (error) {
        throw new Error(errorHandler(error));
      }
    },
    onSuccess: (response) => {
      alert("Nalog je aktiviran! Sada se možete prijaviti.");
      queryClient.invalidateQueries({ queryKey: ['activateAccount'] });
    }
  });
}

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
      queryClient.invalidateQueries({ queryKey: ['forgotPassword'] });
    }
  });
}

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
      queryClient.invalidateQueries({ queryKey: ['resetPassword'] });
    }
  });
}