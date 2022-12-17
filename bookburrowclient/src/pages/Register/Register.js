import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Form, Button, InputGroup } from "react-bootstrap";
import logo from "./BookBurrowLogo.png";
import RegisterModal from "./components/RegisterModal";

export default function RegisterUser({ user, setCurrentUser }) {
  const [userProfileImage, setUserProfileImage] = useState(user.photoURL);
  const [userFirstName, setUserFirstName] = useState(user.displayName);
  const [userLastName, setUserLastName] = useState(user.displayName);
  const [userHandle, setUserHandle] = useState("");
  const [userPronounId, setUserPronounId] = useState(3);
  const [userPronouns, setUserPronouns] = useState("");
  const [userBiography, setUserBiography] = useState("");
  const [userBiographyUrl, setUserBiographyUrl] = useState("");
  const [date, setDate] = useState(new Date()); //user birthday
  
  const [pronouns, syncPronouns] = useState([]); //state variable for array of pronouns
    
  const navigation = useNavigate();
  
  const navigateToDashboard = () => {
    navigation('/')
  }

    //Fetch all pronouns
    useEffect(() => {
        fetch(`https://localhost:7210/api/UserPronoun`, {
            method: "GET",
            headers: {
              "Access-Control-Allow-Origin": "https://localhost:7210",
              "Content-Type": "application/json",
            },
        })
        .then((res) => res.json())
        .then((data) => {
            syncPronouns(data);
        })
    }, []);

  const RegisterUser = () => {
    const newRegistrationObject = {
      user: {
        email: user.email,
        firebaseUID: user.uid
      },
      userProfile: {
        userId: user.id,
        profileImageUrl: userProfileImage,
        firstName: userFirstName,
        lastName: userLastName,
        handle: userHandle,
        pronounId: userPronounId,
        userPronoun: {
          id: userPronounId,
          pronouns: userPronouns,
        },
        biography: userBiography,
        biographyUrl: userBiographyUrl,
        birthday: date,
      }
    }
    const fetchOptions = {
      method: 'POST',
      headers: {
        "Access-Control-Allow-Origin": "https://localhost:7210",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(newRegistrationObject)
    }
    return fetch('https://localhost:7210/api/User', fetchOptions)
    .then(res => res.json())
    .then((res) => {
      console.log(res)
       setCurrentUser(res)
    })
    .then((res) => {
      setUserProfile(res)
    })
    .then(() => {navigateToDashboard()})
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
          <Form.Control type="text" placeholder="Photo url" defaultValue={user.photoURL} onChange={(event) => setUserProfileImage(event.target.value)}/>
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicFirstName">
          <Form.Label>First Name</Form.Label>
          <Form.Control type="text" placeholder="First name" defaultValue={user.displayName} onChange={(event) => setUserFirstName(event.target.value)} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicLastName">
          <Form.Label>Last Name</Form.Label>
          <Form.Control type="text" placeholder="Last name" defaultValue={user.displayName} onChange={(event) => setUserLastName(event.target.value)} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicHandle">
          <Form.Label>Handle</Form.Label>
          <InputGroup>
          <InputGroup.Text id="inputGroupPrepend">@</InputGroup.Text>
          </InputGroup>
          <Form.Control type="text" placeholder="handle" onChange={(event) => setUserHandle(event.target.value)} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicPronouns">
          <Form.Control as="select" onChange={
            (event) => {
              setUserPronounId(parseInt(event.target.value))
              setUserPronouns(event.target.options[event.target.selectedIndex].innerHTML)
              }
          }
            value={userPronounId}>
            {pronouns.map((e) => (
                <option key={`pronoun--${e.id}`}
                value={e.id}
                >
                    {e.pronouns}
                </option>
            ))}
          </Form.Control>
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicBiography">
          <Form.Label>Biography</Form.Label>
          <Form.Control as="textarea" rows={3} onChange={event => setUserBiography(event.target.value)} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicBiographyUrl">
          <Form.Label>Featured Link (url)</Form.Label>
          <Form.Control type="text" placeholder="e.g., www.yourawesomewebsite.com" onChange={event => setUserBiographyUrl(event.target.value)} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicBirthday">
        <Form.Label>Birthday</Form.Label>
          <Form.Control type="date" placeholder="Date of birth" value={date} onChange={(e) => setDate(e.target.value)} />
        </Form.Group>
      </Form>
      <Button type="submit" onClick={() => {RegisterUser()}}>Register</Button>
    </> : null
  );
}
