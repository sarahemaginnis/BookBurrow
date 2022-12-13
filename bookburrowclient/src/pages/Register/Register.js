import React, { useState, useEffect } from "react";

export default function RegisterUser({ user, setCurrentUser }) {
    
      const RegisterUser = () => {
        const newUser = {
          email: user.email,
          firebaseUID: user.uid
        }
        const fetchOptions = {
          method: 'POST',
          headers: {
            "Access-Control-Allow-Origin": "https://localhost:7210",
            "Content-Type": "application/json",
          },
          body: JSON.stringify(newUser)
        }
        return fetch('https://localhost:7210/api/User', fetchOptions)
        .then(res => res.json())
        .then((res) => {
            setCurrentUser(res)
        })
      }

  return (
    user ?
    <>
      <h1>Register page</h1>
      <h3>{user.displayName}</h3>
      <p>Verify your information below:</p>
      <p>{user.email}</p>
      <p>Form forthcoming!</p>
      <button type="submit" onClick={() => RegisterUser()}>Register</button>
    </> : null
  );
}
