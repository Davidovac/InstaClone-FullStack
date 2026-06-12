import React from "react";
import styles from './SendCommentInput.module.scss';

const SendCommentInput = ({comment, setComment, onSend}) => {
  return (
    <div className={styles.sendCommentInput}>
      <input type="text" value={comment} onChange={(e) => setComment(e.target.value)}/>
      <button onClick={onSend}>Send</button>
    </div>
  );
};

export default SendCommentInput;