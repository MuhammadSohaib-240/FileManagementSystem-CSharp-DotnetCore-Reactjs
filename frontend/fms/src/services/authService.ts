import axios, { AxiosResponse } from 'axios';

const API_URL = 'https://localhost:7055/api/auth';

interface LoginResponse {
  message: string;
}

interface RegisterResponse {
  message: string;
}

axios.defaults.withCredentials = true;

export const login = async (email: string, password: string): Promise<void> => {
  try {
    const response: AxiosResponse<LoginResponse> = await axios.post(`${API_URL}/login`, { email, password });
    
    if (response.data.message === 'success') {
      localStorage.setItem('jwt', response.data.message);
    } else {
      throw new Error('Login failed');
    }
  } catch (e) {
    console.error('Login error:', e);
    throw new Error('Login failed');
  }
};

export const logout = async (): Promise<void> => {
  try {
    // Make a POST request to logout endpoint
    const response = await axios.post(`${API_URL}/logout`, {}, { withCredentials: true });

    // Check if the response indicates success
    if (response.status === 200) {
      // Remove token from local storage
      localStorage.removeItem('jwt');
      
      // Clear all cookies
      const cookies = document.cookie.split(';');
      for (let i = 0; i < cookies.length; i++) {
        const cookie = cookies[i];
        const eqPos = cookie.indexOf('=');
        const name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
        document.cookie = name + '=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/';
      }
    } else {
      throw new Error('Logout failed');
    }
  } catch (error) {
    console.error('Logout error:', error);
    throw new Error('Logout failed');
  }
};

export const register = async (name: string, username: string, email: string, password: string): Promise<void> => {
  try {
    const response: AxiosResponse<RegisterResponse> = await axios.post(`${API_URL}/register`, { name, username, email, password });
    
    if (response.data.message === 'success') {
    } else {
      throw new Error('Registration failed');
    }
  } catch (e) {
    console.error('Registration error:', e);
    throw new Error('Registration failed');
  }
};
