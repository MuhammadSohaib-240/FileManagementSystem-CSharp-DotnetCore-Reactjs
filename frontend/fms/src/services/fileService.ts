import axios from 'axios';

const API_URL = 'https://localhost:7055/api/files';

// Define the FileDetails interface
interface FileDetails {
  id: number;
  name: string;
  length: number;
  creationTime: string;
  lastAccessTime: string;
  lastWriteTime: string;
  filePath: string;
}

// Define the function to upload a file
export const uploadFile = async (file: File, customFileName: string): Promise<FileDetails> => {
  const formData = new FormData();
  formData.append('File', file);
  formData.append('CustomFileName', customFileName);

  try {
    const response = await axios.post<FileDetails>(`${API_URL}/upload`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  } catch (error) {
    console.error('Error uploading file:', error);
    throw error;
  }
};

// Define the function to fetch files
export const getFiles = async (): Promise<FileDetails[]> => {
  try {
    const response = await axios.get<FileDetails[]>(API_URL);
    return response.data;
  } catch (error) {
    console.error('Error fetching files:', error);
    throw error;
  }
};

export const deleteFile = async (id: number) => {
  try {
    const response = await axios.delete(`${API_URL}/${id}`);
    return response.data;
  } catch (error) {
    console.error('Error deleting file:', error);
    throw error;
  }
};

export const renameFile = async (id: number, newFileName: string): Promise<FileDetails> => {
  const response = await axios.put(`${API_URL}/rename/${id}`, { newFileName });
  return response.data;
};