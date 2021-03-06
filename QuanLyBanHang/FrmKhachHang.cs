﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;

namespace QuanLyBanHang
{
    public partial class FrmKhachHang : Form
    {
        KhachHangBUS KHBUS = new KhachHangBUS();
        public FrmKhachHang()
        {
            InitializeComponent();
        }

        private void FrmKhachHang_Load(object sender, EventArgs e)
        {
            List<KhachHang> list = KHBUS.LoadKhachHang();
            dgvKhachHang.DataSource = list;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string id, name, address;
            string phonenumber, fax;
            id = txtMa.Text;
            name = txtTen.Text;
            address = txtDiaChi.Text;
            phonenumber = txtSdt.Text;
            fax = txtSofax.Text;
            KhachHang c = new KhachHang(id, name, address, phonenumber, fax);

            int NumberOfRows = KHBUS.Add(c);
            if (NumberOfRows > 0)
            {
                List<KhachHang> list = KHBUS.LoadKhachHang();
                dgvKhachHang.DataSource = list;
                MessageBox.Show("Thêm " + NumberOfRows + " khách hàng thành công" );
            }
            else
                MessageBox.Show("Thêm thất bại");
        }

        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex;
            if (dgvKhachHang.Columns[col] is DataGridViewButtonColumn && dgvKhachHang.Columns[col].Name == "CotXoa")
            {
                int row = e.RowIndex;
                string id = dgvKhachHang.Rows[row].Cells["CotCanLay"].Value.ToString();
                int NumberOfRows = KHBUS.Delete(id);
                if (NumberOfRows > 0)
                {
                    List<KhachHang> list = KHBUS.LoadKhachHang();
                    dgvKhachHang.DataSource = list;
                    MessageBox.Show("Xóa " + NumberOfRows + " khách hàng thành công");
                }
                else
                    MessageBox.Show("Xóa thất bại");
            }
            else if (dgvKhachHang.Columns[col] is DataGridViewButtonColumn && dgvKhachHang.Columns[col].Name == "cotSua")
            {
                int row = e.RowIndex;
                string id = dgvKhachHang.Rows[row].Cells["CotCanLay"].Value.ToString();
                KhachHang kh = KHBUS.GetById(id);
                kh.TenKH = txtTen.Text;
                kh.DiaChi = txtDiaChi.Text;
                kh.DienThoai = txtSdt.Text;
                kh.Fax = txtSofax.Text;
                
                int number = KHBUS.Update(kh);

                List<KhachHang> list = KHBUS.LoadKhachHang();
                dgvKhachHang.DataSource = list;
            }
        }
    }
}
