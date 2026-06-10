import api from "./Api";

export const userService = {
  getAll: async () => {
    const res = await api.get('/users');
    return res.data;
  },

  create: async (name)=> {
    const res = await api.post('/users', { name });
    return res.data;
  },

  update: async (payload) => {
    const { id, ...body } = payload;
    const res = await api.put(`/users/${id}`, body)
    return res.data
  },

  delete: async (id) => {
    const res = await api.delete(`/users/${id}`)
  }
};