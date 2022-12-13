import React from 'react';
import { signIntoFirebase, signOutOfFirebase } from '../utils/auth';
import { useAuth } from '../utils/context/authContext';

            //check if they exist in my database Fetch /loginviewmodel/verifyuser
            //inside of response, .then if response.status == 200 redirect to home
            //if response.status == 404 is not found, redirect to registration page
            //otherwise, redirect to homepage
export default function Authenticated({ user }) {
    const { user, userLoading, uid } = useAuth();
    const buttonClick = () => {
        if (user){
            signOutOfFirebase()
        } else {
            signIntoFirebase()
            .then(()=> {console.log(user)
            fetch(`https://localhost:7210/api/LoginViewModel/VerifyUser/${uid}`)})
            .then((data) => {
                if(data.status == 200){
                    //redirect to homepage
                    console.log("User found, going to homepage")
                } else if(data.status == 404){
                    //redirect to registration page
                    console.log("User not found, going to registration")
                }
            }).catch(error => console.log(error))
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
      <div className="mt-2">
        <button type="button" className="btn btn-danger" onClick={buttonClick}>
          {user ? "Sign Out" : "Sign In"}
        </button>
      </div>
    </div>
  );
}