import "../styles/About.scss";

import img from "../images/img_white.png";

const AboutUs = () => {
  return (
    <div className="about-us-container">
      <h1>Group Members Details</h1>
      <div className="team-members">
        <div className="team-member-card">
          <img src={img} alt="Team Member 1" />
          <h2>Muhammad Sohaib</h2>
          <p>Roll No: 2021F-BSE-213</p>
          <p>Role: Frontend & Backend Development</p>
        </div>
        <div className="team-member-card">
          <img src={img} alt="Team Member 1" />
          <h2>Muhammad Mudassir</h2>
          <p>Roll No: 2021F-BSE-199</p>
          <p>Role: Design & Testing</p>
        </div>
      </div>
    </div>
  );
};

export default AboutUs;
