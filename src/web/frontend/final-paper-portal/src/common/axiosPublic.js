import axios from 'axios'

export const axiosPublic = axios.create({
  baseURL: 'https://localhost:7169/api/v1/',
  headers: {
    Accept: 'application/json',
    'Content-Type': 'application/json'
  }
})
