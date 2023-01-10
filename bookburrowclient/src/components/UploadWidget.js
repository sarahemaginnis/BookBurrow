import React, { useEffect, useRef } from "react";
import { Button } from "react-bootstrap";

const UploadWidget = (props) => {
    const cloudinaryRef = useRef();
    const widgetRef = useRef();
    useEffect(() => {
        cloudinaryRef.current = window.cloudinary;
        widgetRef.current = cloudinaryRef.current.createUploadWidget({
            cloudName: 'dxblkicjh',
            uploadPreset: 'bookBurrow'
        }, function(error, result){
            console.log(result);
            console.log(result.info.files);
            console.log(result.info.files[0].uploadInfo.secure_url); //this url needs to be sent to the backend - how do I pass this up to parent component?
            props.func(result.info.files[0].uploadInfo.secure_url);
        });
    }, [])
    return(
        <Button onClick={() => widgetRef.current.open()}>
            Upload Image
        </Button>
    )
}

export default UploadWidget;