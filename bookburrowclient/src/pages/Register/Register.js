import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Form, Button, InputGroup } from "react-bootstrap";
import logo from "./BookBurrowLogo.png";
import RegisterModal from "./components/RegisterModal";
import PronounsDropDown from "./components/PronounsDropDown";

export default function RegisterUser({ user, setCurrentUser }) {
  const [userProfileImage, setUserProfileImage] = useState(user.photoURL);
  const [userFirstName, setUserFirstName] = useState(user.displayName);
  const [userLastName, setUserLastName] = useState(user.displayName);
  const [userHandle, setUserHandle] = useState("");
  const [userPronounId, setUserPronounId] = useState(3);
  const [userBiography, setUserBiography] = useState("");
  const [userBiographyUrl, setUserBiographyUrl] = useState("");
  const [date, setDate] = useState(new Date()); //user birthday
  
  const [pronouns, syncPronouns] = useState([]); //state variable for array of pronouns
    
  const navigation = useNavigate();
  
  const navigateToDashboard = () => {
    navigation('/dashboard')
  }

  const setUserPronouns = (userPronounId) => {
    return pronouns.find(obj => obj.id === userPronounId)
  }

  //Fetch all pronouns
  useEffect(() => {
    fetch (`https://localhost:7210/api/UserPronoun`)
    .then((res) => res.json())
    .then(syncPronouns);
  }, []);

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

  const RegisterProfile = () => {
    const newUserProfile = {
      userId: user.id,
      profileImageUrl: userProfileImage,
      firstName: userFirstName,
      lastName: userLastName,
      handle: userHandle,
      pronounId: userPronounId,
      userPronoun: {
        id: userPronounId,
        pronouns: setUserPronouns(userPronounId),
      },
      biography: userBiography,
      biographyUrl: userBiographyUrl,
      birthday: date,
    };

    const fetchOptions = {
      method: 'POST',
      headers: {
        "Access-Control-Allow-Origin": "https://localhost:7210",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(newUserProfile)
    }
    return fetch('https://localhost:7210/api/UserProfile', fetchOptions)
      .then(res => res.json())
      .then((res) => {
        setUserProfile(res)
      })
      .then(() => {navigateToDashboard()} )
  }

  //need to check userHandle to see if it's available

  return (
    user ?
    <>
    {/*<RegisterModal />*/}
      <img
        className="image__login"
        src={logo}
        alt="Book Burrow Logo"
      />
      <Form>
        <Form.Group className="mb-3" controlId="formBasicPhotoUrl">
          <Form.Label>Profile Image</Form.Label>
          <Form.Control type="text" placeholder="Photo url" defaultValue={user.photoURL} onChange={setUserProfileImage}/>
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicFirstName">
          <Form.Label>First Name</Form.Label>
          <Form.Control type="text" placeholder="First name" defaultValue={user.displayName} onChange={setUserFirstName} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicLastName">
          <Form.Label>Last Name</Form.Label>
          <Form.Control type="text" placeholder="Last name" defaultValue={user.displayName} onChange={setUserLastName} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicHandle">
          <Form.Label>Handle</Form.Label>
          <InputGroup>
          <InputGroup.Text id="inputGroupPrepend">@</InputGroup.Text>
          </InputGroup>
          <Form.Control type="text" placeholder="handle" onChange={setUserHandle} />
        </Form.Group>
        <PronounsDropDown label={"Preferred Pronouns"} optionList={pronouns} onChange={setUserPronounId} value={userPronounId} />
        <Form.Group className="mb-3" controlId="formBasicBiography">
          <Form.Label>Biography</Form.Label>
          <Form.Control as="textarea" rows={3} onChange={setUserBiography} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicBiographyUrl">
          <Form.Label>Featured Link (url)</Form.Label>
          <Form.Control type="text" placeholder="e.g., www.yourawesomewebsite.com" onChange={setUserBiographyUrl} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicBirthday">
        <Form.Label>Birthday</Form.Label>
          <Form.Control type="date" placeholder="Date of birth" value={date} onChange={(e) => setDate(e.target.value)} />
        </Form.Group>
      </Form>
      <Button type="submit" onClick={() => {RegisterUser(); RegisterProfile();}}>Register</Button>
    </> : null
  );
}
