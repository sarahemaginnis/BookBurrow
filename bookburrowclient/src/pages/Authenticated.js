import React from 'react';
import { signIntoFirebase, signOutOfFirebase } from '../utils/auth';
            //check if they exist in my database Fetch /loginviewmodel/verifyuser
            //inside of response, .then if response.status == 200 redirect to home
            //if response.status == 404 is not found, redirect to registration page
            //otherwise, redirect to homepage
export default function Authenticated({ user, currentUser }) {

    const buttonClick = () => {
        if (user){
            signOutOfFirebase()
        } else {
            signIntoFirebase()
        }
    }

  return (
    <div className="text-center mt-5">
      <h1>Welcome, {user.displayName}!</h1>
      <img
        referrerPolicy="no-referrer"
        src={user.photoURL}
        alt={user.displayName}
      />
      <h1>{currentUser.name}</h1>
      <h5>{currentUser.email}</h5>
      <div className="mt-2">
        <button type="button" className="btn btn-danger" onClick={buttonClick}>
          {user ? "Sign Out" : "Sign In"}
        </button>
      </div>
    </div>
  );
}