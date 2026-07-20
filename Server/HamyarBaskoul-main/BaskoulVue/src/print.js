export function printTripleBarge(id, { removeAfterMs = 1000 } = {}) {
  const existingFrame = document.getElementById("triple-barge-print-frame");
  if (existingFrame) existingFrame.remove();

  const iframe = document.createElement("iframe");
  iframe.id = "triple-barge-print-frame";
  iframe.style.position = "fixed";
  iframe.style.right = "0";
  iframe.style.bottom = "0";
  iframe.style.width = "0";
  iframe.style.height = "0";
  iframe.style.border = "0";
  iframe.style.opacity = "0";
  iframe.setAttribute("aria-hidden", "true");

  iframe.onload = () => {
    try {
      const frameWindow = iframe.contentWindow;
      if (!frameWindow) return;
      frameWindow.focus();
      frameWindow.print();
      window.setTimeout(() => iframe.remove(), removeAfterMs);
    } catch {
      iframe.remove();
    }
  };

  const url = new URL(
    `/Reports/PrintTripleBarge/${id}?silent=1`,
    window.location.origin,
  );
  iframe.src = url.toString();
  document.body.appendChild(iframe);
}
