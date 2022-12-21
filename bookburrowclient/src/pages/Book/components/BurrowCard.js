// const[state, setButtonStatus]=useState("1")
// {return<>
//   <View>
//         {state == "1" ? (
//           <Text>First View</Text>
//         ) : state == "2" ? (
//           <Text>2nd View</Text>
//         ) : state == "3" ? (
//           <Text>3rd View</Text>
//         ) : (
//           <Text>4th View.</Text>
//         )}
//       </View>
// </>
// }
import React, { useState } from "react";
import { Button, Col, Row } from "react-bootstrap";
import BurrowPostGrid from "../../../components/burrow/Burrow";
import "./BurrowCard.css";


const BurrowCard = ({book, user, currentUser, userBook, userProfile}) => {
    const [posts, syncPosts] = useState([]); //State variable for array of posts

    //Fetch all bookPosts
    //map through book posts and then pass in props to Burrow: user, post

    return (
        <div className="burrow-card">
            <Row>
                <Col sm={4}>
                </Col>
                <Col sm={8}>
                    <Row>
                        <Col>
                            <h3>Latest</h3>
                        </Col>
                        <Col>
                            <h3>Top</h3>
                        </Col>
                    </Row>
                    <p>Insert posts card</p>
                    {/*<BurrowPostGrid user={user} post={post} /> */}
                </Col>
            </Row>
        </div>
    );
};
export default BurrowCard;