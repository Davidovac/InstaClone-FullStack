import api from "./Api";

export const authService = {
  login: async (userName, password) => {

    const loginData = {
      userName: userName,
      password: password
    }
    
    return await api.post("/auth/login", loginData);
  },

  register: async (userName, password, email, firstName, lastName) => {

    const registerData = {
      userName: userName,
      password: password,
      email: email,
      firstName: firstName,
      lastName: lastName
    }
    
    return await api.post("/auth/register", registerData);
  },

  activateAccount: async (email, token) => {

    const activationData = {
      email: email,
      token: token
    }

    return await api.post(`/auth/activate-account`, activationData);
  },

  forgotPassword: async (data) => {

    return await api.post("/auth/forgot-password", { email: data.email });
  },

  resetPassword: async (data) => {

    const resetPasswordData = {
      email: data.email,
      token: data.token,
      newPassword: data.newPassword,
      confirmPassword: data.confirmPassword
    }

    return await api.post("/auth/reset-password", resetPasswordData);
  }
}