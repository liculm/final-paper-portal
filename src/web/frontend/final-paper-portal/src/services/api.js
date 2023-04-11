import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://localhost:7169/api/v1/',
  headers: {
    Accept: 'application/json',
    'Content-Type': 'application/json'
  }
});

export default {
  async getResource () {
    const response = await apiClient.get('/resource'); // replace with the endpoint of your .NET API
    return response.data;
  },
  async login (data) {
    const response = await apiClient.post('user/login', data); // replace with the endpoint of your .NET API
    return response.data;
  }// add additional API methods as needed
};
