import React from 'react'
import ReactDOM from 'react-dom/client'
import {
  BrowserRouter,
  Navigate,
  Routes,
  Route
} from 'react-router-dom'
import App from './App.jsx'
import FetchEmployees from './Employees.jsx'
import './index.css'



const app = (
  <BrowserRouter>
    <Routes>
      <Route path="/" element={<App />} />
      <Route path="/employees" element={<FetchEmployees />} />
    </Routes> 
  </BrowserRouter>
);

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    {app}
  </React.StrictMode>,
)
