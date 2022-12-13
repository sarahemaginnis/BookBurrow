import React, { useState, useEffect } from "react";

export default function RegisterUser({ user }) {
  const [newUser, setNewUser] = useState({});

  const RegisterUser = (user) => {
    const newUser = {
      name: user.displayName,
      email: user.email,
      firebaseId: user.uid,
      image: user.photoURL,
    };
    const fetchOptions = {
      method: "POST",
      headers: {
        "Access-Control-Allow-Origin": "https://localhost:7210",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(newUser),
    };
    return fetch("https://localhost:7210/api/User", fetchOptions)
      .then((res) => res.json())
      .then(() => {
        history.push("/");
      });
  };

  return (
    <>
      <h1>Register page</h1>
      <h3>{user.displayName}</h3>
    </>
  );
}
