import React from 'react';
import styles from './Avatar.module.scss';

const Avatar = ({ avatar }) => {
  return(
    <div className={styles.avatar}>
      <img className="profilePic" src={avatar && avatar.length > 10 
        ? `http://localhost:5231${avatar}`
        : 'http://localhost:5231/images/defaults/defAvatar.jpg'} />
    </div>
  );
}

export default Avatar;