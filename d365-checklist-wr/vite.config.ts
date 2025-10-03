import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  build: {
    // Tạo tên file cố định để dễ deploy lên D365
    rollupOptions: {
      output: {
        entryFileNames: 'assets/checklist-main.js',
        chunkFileNames: 'assets/checklist-[name].js',
        assetFileNames: 'assets/checklist-[name].[ext]'
      }
    },
    // Để debug dễ hơn
    sourcemap: true,
    // Tối ưu cho environment sản xuất
    minify: 'terser',
  },
  // Cấu hình base path cho D365
  base: './',
  // Preview port
  preview: {
    port: 5173
  }
})
