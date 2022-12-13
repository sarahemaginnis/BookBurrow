import React, {useState, useEffect} from 'react';
import Loading from '../components/Loading';
import LogIn from '../pages/LogIn/LogIn';
import NavBar from '../components/Navbar';
import { useAuth } from '../utils/context/authContext';
import AppRoutes from '../routes/AppRoutes';
import RegisterUser from '../pages/Register/Register';

function Initialize() {
  const { user, userLoading, userRegistering } = useAuth();

  // if user state is null, then show loader
  if (userLoading) {
    return <Loading />;
  }
  return <>{user ? (<><NavBar user={user} /> <AppRoutes user={user} /></>) : <LogIn />}</>;
}

export default Initialize;