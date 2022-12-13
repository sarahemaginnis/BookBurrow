import firebase from 'firebase/app';
import 'firebase/auth';

const signIntoFirebase = () => {
  const provider = new firebase.auth.GoogleAuthProvider();
  return firebase.auth().signInWithPopup(provider);
};

const signOutOfFirebase = () => {
  firebase.auth().signOut();
};

export { signIntoFirebase, signOutOfFirebase };