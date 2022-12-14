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
import React from "react";
import { Button, Col, Row } from "react-bootstrap";
import "./BurrowCard.css";


const BurrowCard = ({book, user, currentUser}) => {

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
                </Col>
            </Row>
        </div>
    );
};
export default BurrowCard;