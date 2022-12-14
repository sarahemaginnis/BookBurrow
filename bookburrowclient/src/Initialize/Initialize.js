import React from 'react';
import Loading from '../components/Loading';
import LogIn from '../pages/LogIn/LogIn';
import NavBar from '../components/navbar/Navbar';
import { useAuth } from '../utils/context/authContext';
import AppRoutes from '../routes/AppRoutes';

function Initialize() {
  const { user, userLoading } = useAuth();

  // if user state is null, then show loader
  if (userLoading) {
    return <Loading />;
  }
  return <>{user ? (<><NavBar user={user} /> <AppRoutes user={user} /></>) : <LogIn />}</>;
}

export default Initialize;