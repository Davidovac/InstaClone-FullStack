export function errorHandler(error) {
  const response = error?.response;
  const data = response?.data;

  const message =
    data?.detail ||
    data?.message ||
    (Array.isArray(data?.errors) && data.errors.join('; ')) ||
    error?.message ||
    "Server error";

  console.error(message);
  return message;
}