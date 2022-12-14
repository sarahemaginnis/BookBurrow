import React from "react";
import { Form } from "react-bootstrap";

const PronounsDropDown = ({ label, optionList, onChange, value }) => {
  return (
    <Form.Group className="mb-3" controlId="formBasicPronouns">
          <Form.Select aria-label={label} value={value} onChange={(event) => onChange(parseInt(event.target.value))}>
            {optionList.map((e) => (
                <option key={`pronoun--${e.id}`}
                value={e.id}
                >
                    {e.pronouns}
                </option>
            ))}
          </Form.Select>
        </Form.Group>
  );
};

export default PronounsDropDown;