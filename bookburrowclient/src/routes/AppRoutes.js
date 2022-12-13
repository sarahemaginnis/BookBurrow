// index for router
import React from 'react';
import { Route, Routes } from 'react-router-dom';
import Authenticated from '../pages/Authenticated';

export default function AppRoutes({ user }) {
  return (
    <div>
      <Routes>
        <Route exact path="/" element={<Authenticated user={user} />} />
        <Route path="*" element={<Authenticated user={user} />} /> {/*leave this at the bottom of the list; return them to home*/}
      </Routes>
    </div>
  );
}