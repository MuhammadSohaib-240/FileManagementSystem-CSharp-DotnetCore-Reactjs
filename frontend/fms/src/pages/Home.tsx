// Home.tsx
import React from "react";
import "../styles/Home.scss";
import logo from "../images/SSUET_Logo.png";
import { Link } from "react-router-dom";

const Home: React.FC = () => {
  return (
    <div className="landing-page">
      <header className="header">
        <div className="logo-container">
          <img src={logo} alt="Logo" className="logo" />
        </div>
        <div className="nav-links-container">
          <Link className="link" to="/about">
            Go to About Us
          </Link>
          <Link className="link" to="/login">
            Login
          </Link>
          <Link className="link" to="/register">
            Register
          </Link>
        </div>
      </header>
      <div className="content">
        <div className="text-container">
          <h1>File Management System</h1>
          <p>
          Discover the future of file management with our innovative solution designed to streamline your digital workspace. Effortlessly upload, view, and organize your files, whether they're PDFs, images, or documents. Our platform offers seamless search and filtering options, allowing you to quickly locate the files you need. Experience the convenience of downloading files with a single click and securely managing your content. With intuitive upload functionality, you can add new files in seconds. Simplify your file management process with our dynamic, user-friendly interface – efficient, secure, and designed for your productivity.
          </p>
          <Link
            className="link"
            to="files"
          >
            Try Now
          </Link>
        </div>
        <div className="video-container">
          <img src={logo} alt="" />
          <h2>Sir Syed University of Engineering & Technology</h2>
          <h3>2024 Web Engineering Project</h3>
        </div>
      </div>
    </div>
  );
};

export default Home;
