import React, { useEffect, useRef } from "react";
import { Button } from "react-bootstrap";

const UploadWidget = () => {
    const cloudinaryRef = useRef();
    const widgetRef = useRef();
    useEffect(() => {
        cloudinaryRef.current = window.cloudinary;
        widgetRef.current = cloudinaryRef.current.createUploadWidget({
            cloudName: 'dxblkicjh',
            uploadPreset: 'bookBurrow'
        }, function(error, result){
            console.log(result);
        });
    }, [])
    return(
        <Button onClick={() => widgetRef.current.open()}>
            Upload
        </Button>
    )
}

export default UploadWidget;