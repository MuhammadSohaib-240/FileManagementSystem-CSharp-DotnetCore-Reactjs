import { Route, Routes } from 'react-router-dom'
import './App.css'
import { FileUploadPage } from './pages/FileUploadPage'
import Home from './pages/Home'
import AboutUs from './pages/About'
import FileListPage from './pages/FileListPage'
import { ToastContainer} from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Login from './pages/LoginPage'
import Register from './pages/RegistrationPage'


function App() {

  return (
    <>
      <div className="app">
      <ToastContainer />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/about" element={<AboutUs />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/upload" element={<FileUploadPage />} />
          <Route path="/files" element={<FileListPage />} />
        </Routes>
      </div>
    </>
  )
}

export default App
