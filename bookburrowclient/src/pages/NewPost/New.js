import React, { useState } from "react";
import './New.css';
import NewPost from "../../components/new/NewPostComponent";

export default function New ({user, currentUser}) {
    return(
        <>
            <NewPost />
        </>
    )
}