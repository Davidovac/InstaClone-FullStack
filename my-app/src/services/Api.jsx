import axios from 'axios';
import { useAuthStore } from '../store/useAuthStore';

const api = axios.create({
  baseURL: 'http://localhost:5231/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

api.interceptors.request.use((config) => {
  const token = useAuthStore.getState().token;

  if (token && config.headers) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  if (config.data instanceof FormData && config.headers) {
    delete config.headers['Content-Type'];
  }

  return config;
},
(error) => Promise.reject(error)
);

api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      console.log('Niste autorizovani, molimo prijavite se ponovo.');
      useAuthStore.getState().logout();
      return Promise.reject(error);
    }

    let customError = {
      status: error.response?.status || 500,
      message: 'An unexpected error occurred',
      details: null,
    };

    if (error.response) {
      customError.message = error.response.data?.message || 'Server Error';
      customError.details = error.response.data?.details || null;
    } else if (error.request) {
      customError.message = 'No response from server. Check your connection.';
    } else {
      customError.message = error.message;
    }
    console.error(customError);
    return Promise.reject(customError);
  }
);

export default api;