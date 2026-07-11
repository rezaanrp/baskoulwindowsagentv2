const script = document.querySelector('script[src*="/baskoul-vue/"]')
const scriptUrl = script ? new URL(script.src) : new URL(location.href)
const marker = '/baskoul-vue/'
const markerIndex = scriptUrl.pathname.toLowerCase().indexOf(marker)
export const pathBase = markerIndex >= 0 ? scriptUrl.pathname.slice(0, markerIndex) : ''

let antiforgeryToken = ''

async function ensureToken() {
  if (antiforgeryToken) return antiforgeryToken
  const response = await fetch(`${pathBase}/api/baskoul/antiforgery`, { credentials: 'same-origin' })
  if (!response.ok) throw new Error('دریافت توکن امنیتی انجام نشد.')
  antiforgeryToken = (await response.json()).token
  return antiforgeryToken
}

export async function api(url, options = {}) {
  const method = (options.method || 'GET').toUpperCase()
  const headers = { Accept: 'application/json', ...(options.headers || {}) }
  if (method !== 'GET') {
    headers['Content-Type'] = 'application/json'
    headers.RequestVerificationToken = await ensureToken()
  }
  const response = await fetch(`${pathBase}/api/baskoul${url}`, { ...options, headers, credentials: 'same-origin' })
  if (!response.ok) {
    const problem = await response.json().catch(() => ({}))
    const validation = problem.errors ? Object.values(problem.errors).flat().join(' ') : ''
    throw new Error(validation || problem.detail || problem.title || 'عملیات انجام نشد.')
  }
  return response.status === 204 ? null : response.json()
}
