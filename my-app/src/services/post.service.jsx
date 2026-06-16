import api from "./Api";

export const postService = {
  getAll: async () => {
    const res = await api.get('/posts');
    return res.data;
  },

  getUserFeed: async () => {
    const res = await api.get('/posts/feed')
    return res.data;
  },

  getPostsByUser: async () => {
    const res = await api.get('/posts/profile')
    return res.data;
  },

  getOne: async (id) => {
    const res = await api.get(`/posts/${id}`);
  },

  create: async (data)=> {
    const res = await api.post('/posts', data);
    return res.data;
  },

  update: async (payload) => {
    const { id, ...body } = payload;
    const res = await api.put(`/posts/${id}`, body);
    return res.data
  },

  delete: async (id) => {
    const res = await api.delete(`/posts/${id}`);
  },

  likePost: async (postId) => {
    const res = await api.post(`/posts/${postId}/like`);
  },

  unlikePost: async (postId) => {
    const res = await api.delete(`/posts/${postId}/like`);
  },

  createComment: async (postId, comment) => {
    const res = await api.post(`/posts/${postId}/comment`, {Text: comment});
  },

  createReplyComment: async (postId, commentId, data) => {
    const res = await api.post(`/posts/${postId}/comment/${commentId}/reply`, {Text: data});
  },
};