import "./SubmitButton.css";

interface SubmitButtonProps {
  text: string;
}

const SubmitButton = ({ text }: SubmitButtonProps) => {
  return (
    <>
      <button type="submit" className="btn">
        {text}
      </button>
    </>
  );
};

export default SubmitButton;
