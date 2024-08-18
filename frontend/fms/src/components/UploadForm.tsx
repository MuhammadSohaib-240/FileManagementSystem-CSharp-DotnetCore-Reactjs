import React, { useState } from "react";
import "../styles/UploadFormOverlay.scss";
import { uploadFile } from "../services/fileService";
import { toast } from "react-toastify";

interface UploadFormProps {
  onClose: () => void;
}

const UploadFormOverlay: React.FC<UploadFormProps> = ({ onClose}) => {
  const [fileName, setFileName] = useState<string>("");
  const [file, setFile] = useState<File | null>(null);

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files && e.target.files.length > 0) {
      setFile(e.target.files[0]);
    }
  };

  const handleUpload = async () => {
    if (!file || !fileName) {
      alert("Please provide a file and a file name.");
      return;
    }

    try {
      const fileDetail = await uploadFile(file, fileName);
      console.log(fileDetail)
      toast.success("File uploaded successfully");
      onClose();
    } catch (error) {
      toast.error("File upload fail");
      console.error("Error uploading file:", error);

    }
  };

  return (
    <div className="upload-overlay">
      <div className="upload-form">
        <h2>Upload File</h2>
        <input
          type="text"
          placeholder="File Name"
          value={fileName}
          onChange={(e) => setFileName(e.target.value)}
        />
        <input type="file" onChange={handleFileChange} />
        <div className="form-buttons">
          <button className="upload-button" onClick={handleUpload}>
            Upload
          </button>
          <button className="cancel-button" onClick={onClose}>
            Cancel
          </button>
        </div>
      </div>
    </div>
  );
};

export default UploadFormOverlay;
