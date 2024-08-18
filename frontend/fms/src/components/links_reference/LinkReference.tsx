import { Link } from "react-router-dom";
import "./LinkReference.css";

interface LinkReferenceProps {
  text: string;
  linkText: string;
  link: string;
}

const LinkReference = ({ text, linkText, link }: LinkReferenceProps) => {
  return (
    <>
      <div className="link">
        <p>
          {text}{" "}
          <Link to={link} className="a">
            {linkText}
          </Link>
        </p>
      </div>
    </>
  );
};

export default LinkReference;
