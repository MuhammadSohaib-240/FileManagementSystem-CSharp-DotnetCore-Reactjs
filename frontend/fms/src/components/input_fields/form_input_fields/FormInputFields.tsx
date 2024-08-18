import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { IconDefinition } from "@fortawesome/free-solid-svg-icons";
import "./FormInputField.css";

interface FormInputProps {
  type: string;
  placeholder: string;
  icon: IconDefinition;
}

const FormInputFields = ({ type, placeholder, icon }: FormInputProps) => {
  return (
    <>
      <div className="input-box">
        <input type={type} placeholder={placeholder} required />
        <FontAwesomeIcon icon={icon} className="i" />
      </div>
    </>
  );
};

export default FormInputFields;
