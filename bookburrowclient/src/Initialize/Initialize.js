import React from 'react';
import Loading from '../components/Loading';
import AppRoutes from '../routes/AppRoutes';
import { useAuth } from '../utils/context/authContext';

function Initialize() {
  const { user, userLoading } = useAuth();

  // if user state is null, then show loader
  if (userLoading) {
    return <Loading />;
  }

  return <AppRoutes user={user} />;
}

export default Initialize;