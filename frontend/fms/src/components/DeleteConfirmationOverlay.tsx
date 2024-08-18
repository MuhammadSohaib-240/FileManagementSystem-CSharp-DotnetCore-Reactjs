import React from "react";
import "../styles/DeleteConfirmationOverlay.scss";

interface DeleteConfirmationOverlayProps {
  onConfirm: () => void;
  onCancel: () => void;
}

const DeleteConfirmationOverlay: React.FC<DeleteConfirmationOverlayProps> = ({
  onConfirm,
  onCancel,
}) => {
  return (
    <div className="overlay">
      <div className="confirmation-box">
        <h2>Are you sure you want to delete this file?</h2>
        <div className="confirmation-buttons">
          <button className="confirm-btn" onClick={onConfirm}>
            Yes
          </button>
          <button className="cancel-btn" onClick={onCancel}>
            No
          </button>
        </div>
      </div>
    </div>
  );
};

export default DeleteConfirmationOverlay;
