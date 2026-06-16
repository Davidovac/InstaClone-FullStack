import api from "./Api";

export const userService = {
  getAll: async () => {
    const res = await api.get('/users');
    return res.data;
  },

  getProfile: async (userName) => {
    const res = await api.get(`/users/${userName}/profile`);
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
  },

  getFollowersByUser: async (id) => {
    const res = await api.get(`/users/${id}/followers`)
    return res.data;
  },

  getFollowingByUser: async (id) => {
    const res = await api.get(`/users/${id}/following`)
    return res.data;
  },

  followProfile: async (userName) => {
    return await api.post(`/users/${userName}/follow-profile`)
  },

  unfollowProfile: async (userName) => {
    return await api.delete(`/users/${userName}/unfollow-profile`)
  },

  followsProfile: async (userName) => {
    const res = await api.get(`/users/${userName}/followed-profile-check`);
    return res.data
  },
};