import api from "./Api";

export const userService = {
  getAll: async () => {
    const res = await apiClient.get('/users');
    return res.data;
  },

  create: async (name)=> {
    const res = await apiClient.post('/users', { name });
    return res.data;
  },
};