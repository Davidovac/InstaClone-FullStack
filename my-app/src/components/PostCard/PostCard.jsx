import React, { useEffect, useRef, useState } from "react";
import styles from "./PostCard.module.scss";
import CommentsSection from "../CommentsSection/CommentsSection";
import PostEngagementDisplay from "../PostEngagementDisplay/PostEngagementDisplay.jsx";
import SendCommentInput from "../SendCommentInput/SendCommentInput";
import Avatar from "../AvatarComponent/Avatar.jsx";
import { useCreateComment, useCreateReplyComment, useLikePost, useUnlikePost } from "../../hooks/usePostQueries.js";

const PostCard = ({ post }) => {
  const [commentsShow, setCommentsShow] = useState(false);
  const [typedComment, setTypedComment] = useState("");
  const [isReply, setIsReply] = useState(false);
  const [repliedCommentId, setRepliedCommentId] = useState(null);
  const [repliedCommentAuthorName ,setRepliedCommentAuthorName] = useState(null);
  const { mutate: createComment, isPending: isSavingComment, isError: isCreateCommentError, error: createCommentError } = useCreateComment();
  const { mutate: createReply, isPending: isSavingReply, isError: isCreateReplyError, error: createReplyError } = useCreateReplyComment();
  const { mutate: like, isPending: isSavingLike, isError: isLikeError, error: likeError } = useLikePost();
  const { mutate: unlike, isPending: isDeletingLike, isError: isUnlikeError, error: enlikeError } = useUnlikePost();
  const [action, setAction] = useState(null); // "like" | "unlike" | null
  const timeoutRef = useRef(null);
  const [isActuallyLiked, setIsActuallyLiked] = useState(false);
  

  useEffect(() => {
    if (!post) return;
    setIsActuallyLiked(!!post?.isLiked)
  },[post])

  useEffect(() => {
    setRepliedCommentAuthorName(null);
    setIsReply(false);
  }, [commentsShow]);

  useEffect(() => {
    if (!action) {
      if (timeoutRef.current) clearTimeout(timeoutRef.current);
      return;
    }

    if (timeoutRef.current) clearTimeout(timeoutRef.current);

    timeoutRef.current = window.setTimeout(async () => {
      if (action === "like") {
        await like(post.id), {
          onSuccess: () => {
            setIsActuallyLiked(true);
          }
        }
      }
      if (action === "unlike") {
        await unlike(post.id), {
          onSuccess: () => {
            setIsActuallyLiked(false);
          }
        }
      }
      setAction(null);
      timeoutRef.current = null;
    }, 3000);

    return () => {
      if (timeoutRef.current) clearTimeout(timeoutRef.current);
    };
  }, [action, like, unlike, post.id]);

  const handleLikeUnlike = () => {
    if (isActuallyLiked) {
      setIsActuallyLiked(false);
    }
    else {
      setIsActuallyLiked(true);
    }
    
    if (!action) {
      setAction(isActuallyLiked ? "unlike" : "like");
    } else {
      setAction(null); // Cancel pending action
    }
  };
  
  const handleSendComment = async () => {
    if (!isReply) {
      await createComment({ postId: post.id, data: typedComment }), {
        onSuccess: (data) => {
          setTypedComment("");
          post.comments.push(data);
        }
      };
    }
    else {
      handleSendReply()
    }
  }

  const handleSendReply = async () => {
    await createReply({postId: post.id, commentId: repliedCommentId, data: typedComment }) , {
      onSuccess: (data) => {
        setTypedReply("");
        post.comments.find(c => c.id == commentId).replies.push(data);

      },
      onSettled: () => {
        setIsReply(false);
        setRepliedCommentAuthorName(null);
      }
    }
    
  }

  const handleReplyAction = (parentId, authorName) => {
    setIsReply(true);
    setRepliedCommentId(parentId);
    setRepliedCommentAuthorName(!authorName ? null : authorName);
  }

  return(
    <div className={styles.postCard}>
      <div className={styles.header}>
        <Avatar avatar={post?.authorPictureUrl} />
        <p><b>{post?.authorName}</b></p>
      </div>

      <div className={styles.imageWrapper}>
        <img src={`http://localhost:5231${post?.photo}`} />
      </div>

      <PostEngagementDisplay 
      setCommentsShow={setCommentsShow} 
      likesCount={post?.likes?.length}
      commentsCount={post?.comments?.length}
      commentsShow={commentsShow}
      authorUsername={post?.authorName}
      caption={post?.caption}
      isLiked={isActuallyLiked}
      onLikeUnlike={handleLikeUnlike}/>

      <div className={styles.commentsSectionContainer}>
        {commentsShow && <CommentsSection comments={post?.comments} replyAction={handleReplyAction}/>}
        <SendCommentInput comment={typedComment} setComment={setTypedComment} 
        onSend={handleSendComment} repliedCommentAuthorName={repliedCommentAuthorName}
        isReply={isReply}/>
      </div>
    </div>
  );
}

export default PostCard;