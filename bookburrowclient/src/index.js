import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter as Router } from 'react-router-dom';
import './styles/main.css';
import Initialize from './Initialize/Initialize';
import AppRoutes from './routes/AppRoutes';

import 'bootstrap/dist/css/bootstrap.min.css';
import { AuthProvider } from './utils/context/authContext';

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <Router>
      <AuthProvider>
        <Initialize />
      </AuthProvider>
    </Router>
  </React.StrictMode>
);