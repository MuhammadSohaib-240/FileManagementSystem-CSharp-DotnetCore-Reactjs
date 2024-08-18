import React, { useEffect, useState } from "react";
import "../styles/FileListPage.scss";
import { useNavigate } from 'react-router-dom';
import UploadFormOverlay from "../components/UploadForm";
import DeleteConfirmationOverlay from "../components/DeleteConfirmationOverlay";
import UpdateFormOverlay from "../components/UpdateForm";
import { getFiles, deleteFile, renameFile } from "../services/fileService";
import { FileDetails } from "../types/types";
import { toast } from "react-toastify";
import { logout } from '../services/authService';


interface File extends FileDetails {
  type: string;
}

const FileListPage: React.FC = () => {
  const [files, setFiles] = useState<File[]>([]);
  const [filter, setFilter] = useState<string | null>(null);
  const [searchTerm, setSearchTerm] = useState<string>("");
  const [showUploadForm, setShowUploadForm] = useState<boolean>(false);
  const [showDeleteConfirmation, setShowDeleteConfirmation] = useState<boolean>(false);
  const [showUpdateForm, setShowUpdateForm] = useState<boolean>(false);
  const [fileToDelete, setFileToDelete] = useState<number | null>(null);
  const [fileToUpdate, setFileToUpdate] = useState<File | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchFiles = async () => {
      try {
        const initialFiles = await getFiles();
        setFiles(
          initialFiles.map((file) => ({
            ...file,
            type: file.name.split(".").pop() || "",
          }))
        );
      } catch (error) {
        toast.error("Error fetching files");
        console.error("Error fetching files:", error);
      }
    };

    fetchFiles();
  }, []);

  const handleDeleteClick = (id: number) => {
    setFileToDelete(id);
    setShowDeleteConfirmation(true);
  };

  const handleConfirmDelete = async () => {
    if (fileToDelete !== null) {
      try {
        const response = await deleteFile(fileToDelete);
        console.log(response);
        setFiles(files.filter(file => file.id !== fileToDelete));
        toast.success("File deleted successfully");
      } catch (error) {
        toast.error("Error deleting file");
        console.error("Error deleting file:", error);
      }
      setShowDeleteConfirmation(false);
      setFileToDelete(null);
    }
  };

  const handleCancelDelete = () => {
    setShowDeleteConfirmation(false);
    setFileToDelete(null);
  };

  const handleDownloadClick = (filepath: string) => {
    window.open(filepath, "_blank");
  };

  const handleUpdateClick = (file: File) => {
    setFileToUpdate(file);
    setShowUpdateForm(true);
  };

  const handleUpdateFile = async (newName: string, fileId: number) => {
    try {
      const updatedFile = await renameFile(fileId, newName);
      setFiles(files.map(file => (file.id === fileId ? { ...file, name: updatedFile.name } : file)));
      toast.success("File renamed successfully");
      setShowUpdateForm(false);
    } catch (error) {
      toast.error("Error renaming file");
      console.error("Error renaming file:", error);
    }
  };

  const handleLogout = async () => {
    try {
      await logout();
      toast.success('Logout successful');
      navigate('/');
    } catch (error) {
      toast.error('Logout failed');
    }
  };

  return (
    <div className="file-container">
      <div className="filter">
        <div className="search-bar">
          <input
            type="text"
            placeholder="Search..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </div>
        <div className="filter-tabs">
          <div
            className={`filter-tab ${filter === null ? "active" : ""}`}
            onClick={() => setFilter(null)}
          >
            All
          </div>
          <div
            className={`filter-tab ${filter === "pdf" ? "active" : ""}`}
            onClick={() => setFilter("pdf")}
          >
            PDFs
          </div>
          <div
            className={`filter-tab ${filter === "image" ? "active" : ""}`}
            onClick={() => setFilter("image")}
          >
            Images
          </div>
          <div
            className={`filter-tab ${filter === "doc" ? "active" : ""}`}
            onClick={() => setFilter("doc")}
          >
            Docs
          </div>
          <button className="filter-tab upload-btn" onClick={() => setShowUploadForm(true)}>
            Upload
          </button>
          <button className="filter-tab upload-btn" onClick={handleLogout}>
            Logout
          </button>
        </div>
      </div>
      <table className="file-table">
        <thead>
          <tr>
            <th>Name</th>
            <th>Type</th>
            <th>Size</th>
            <th>Creation Time</th>
            <th>Last Access Time</th>
            <th>Last Write Time</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {files
            .filter(
              (file) =>
                (filter === null ||
                  (filter === "pdf" && file.type === "pdf") ||
                  (filter === "image" && ["jpg", "jpeg", "png"].includes(file.type.toLowerCase())) ||
                  (filter === "doc" && ["doc", "docx"].includes(file.type.toLowerCase()))) &&
                file.name.toLowerCase().includes(searchTerm.toLowerCase())
            )
            .map((file) => (
              <tr key={file.id}>
                <td>{file.name.substring(0, file.name.lastIndexOf("."))}</td>
                <td>{file.type}</td>
                <td>{(file.length / 1080).toFixed(2)}</td>
                <td>{file.creationTime}</td>
                <td>{file.lastAccessTime}</td>
                <td>{file.lastWriteTime}</td>
                <td className="action-tab">
                  <button className="action-btn update-btn" onClick={() => handleUpdateClick(file)}>Update</button>
                  <button className="action-btn delete-btn" onClick={() => handleDeleteClick(file.id)}>Delete</button>
                  <button className="action-btn download-btn" onClick={() => handleDownloadClick(file.filePath)}>Download</button>
                </td>
              </tr>
            ))}
        </tbody>
      </table>
      {showUploadForm && (
        <UploadFormOverlay
          onClose={() => setShowUploadForm(false)}
        />
      )}
      {showDeleteConfirmation && (
        <DeleteConfirmationOverlay
          onConfirm={handleConfirmDelete}
          onCancel={handleCancelDelete}
        />
      )}
      {showUpdateForm && fileToUpdate && (
        <UpdateFormOverlay
          onClose={() => setShowUpdateForm(false)}
          onUpdate={handleUpdateFile}
          fileId={fileToUpdate.id}
          currentName={fileToUpdate.name}
        />
      )}
    </div>
  );
};

export default FileListPage;
