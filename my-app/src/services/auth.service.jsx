import api from "./Api";

export const authService = {
  login: async (data) => {
    return await api.post("/auth/login", data);
  },

  register: async (data) => {
    return await api.post("/auth/register", data);
  },

  activateAccount: async (data) => {
    return await api.post(`/auth/activate-account`, data);
  },

  forgotPassword: async (data) => {
    return await api.post("/auth/forgot-password", data);
  },

  resetPassword: async (data) => {
    return await api.post("/auth/reset-password", data);
  }
}