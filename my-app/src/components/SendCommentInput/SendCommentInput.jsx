import React from "react";
import styles from './SendCommentInput.module.scss';

const SendCommentInput = ({comment, setComment, onSend, repliedCommentAuthorName}) => {
  return (
    <div className={styles.sendCommentInput}>
      <input type="text"
      value={`${comment}`} onChange={(e) => 
      setComment((text) => (repliedCommentAuthorName && repliedCommentAuthorName != "null" && !text.includes(repliedCommentAuthorName) 
      ? `@${repliedCommentAuthorName} ${e.target.value}` 
      : e.target.value))}/>
      <button onClick={onSend}>Send</button>
    </div>
  );
};

export default SendCommentInput;