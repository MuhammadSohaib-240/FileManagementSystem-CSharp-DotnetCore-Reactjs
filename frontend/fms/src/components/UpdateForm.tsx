import React, { useState } from "react";
import "../styles/UpdateFormOverlay.scss";

interface UpdateFormOverlayProps {
  onClose: () => void;
  onUpdate: (newName: string, fileId: number) => void;
  fileId: number;
  currentName: string;
}

const UpdateFormOverlay: React.FC<UpdateFormOverlayProps> = ({
  onClose,
  onUpdate,
  fileId,
  currentName,
}) => {
  const [newName, setNewName] = useState<string>(currentName);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onUpdate(newName, fileId);
  };

  return (
    <div className="overlay">
      <div className="overlay-content">
        <h2>Update File Name</h2>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="fileName">New Name:</label>
            <input
              type="text"
              id="fileName"
              value={newName}
              onChange={(e) => setNewName(e.target.value)}
            />
          </div>
          <div className="form-buttons">
            <button type="submit">Update</button>
            <button type="button" onClick={onClose}>
              Cancel
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default UpdateFormOverlay;
